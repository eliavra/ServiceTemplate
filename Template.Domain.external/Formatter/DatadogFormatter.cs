using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Template.Domain.external.Formatter
{
    public class DatadogFormatter : ITextFormatter
    {
        private readonly JsonValueFormatter _formatter;

        public DatadogFormatter()
        {
            _formatter = new JsonValueFormatter();
        }

        public void Format(LogEvent logEvent, TextWriter output)
        {

            output.Write("{ \"msgObj\":{");

            output.Write($"\"date\":\"{logEvent.Timestamp.ToUniversalTime().ToString("u")}\",");

            output.Write($"\"level\":\"{ConvertLogLevel(logEvent.Level)}\"");

            output.Write($",\"Message\":");
            var message = logEvent.MessageTemplate.Render(logEvent.Properties);
            JsonValueFormatter.WriteQuotedJsonString(Regex.Replace(message, @"\t|\n|\r", string.Empty), output);

            if (logEvent.Exception != null)
            {
                output.Write(",");
                JsonValueFormatter.WriteQuotedJsonString("exception", output);
                output.Write(":");

                var exValue = JsonConvert.SerializeObject(logEvent.Exception,
                        new JsonSerializerSettings()
                        {
                            StringEscapeHandling = StringEscapeHandling.EscapeNonAscii,
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore,
                            NullValueHandling = NullValueHandling.Ignore,
                            ObjectCreationHandling = ObjectCreationHandling.Reuse,
                            DefaultValueHandling = DefaultValueHandling.Ignore,
                            ContractResolver = new IgnorePropertiesResolver(new[] { "TargetSite", "Source", "HelpURL", "WatsonBuckets" }),
                            MaxDepth = 2,
                        });

                JsonValueFormatter.WriteQuotedJsonString(Regex.Replace(exValue, @"\t|\n|\r", string.Empty), output);
            }

            foreach (var property in logEvent.Properties)
            {
                output.Write(",");
                JsonValueFormatter.WriteQuotedJsonString(property.Key, output);
                output.Write(":");
                _formatter.Format(property.Value, output);
            }

            output.Write("}}");
            output.WriteLine();
        }

        private string ConvertLogLevel(LogEventLevel level)
        {
            switch (level)
            {
                case LogEventLevel.Debug:
                    return "debug";

                case LogEventLevel.Error:
                    return "error";

                case LogEventLevel.Fatal:
                    return "fatal";

                case LogEventLevel.Information:
                    return "info";

                case LogEventLevel.Verbose:
                    return "trace";

                case LogEventLevel.Warning:
                    return "warn";

                default:
                    return "";
            }
        }
    }

    //short helper class to ignore some properties from serialization
    public class IgnorePropertiesResolver : DefaultContractResolver
    {
        private readonly IEnumerable<string> _propsToIgnore;

        public IgnorePropertiesResolver(IEnumerable<string> propNamesToIgnore)
        {
            _propsToIgnore = propNamesToIgnore;
            IgnoreIsSpecifiedMembers = true;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            property.ShouldSerialize = (x) => { return !_propsToIgnore.Contains(property.PropertyName); };
            return property;
        }
    }
}
