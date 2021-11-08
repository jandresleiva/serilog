using BenchmarkDotNet.Attributes;
using Serilog.Capturing;
using Serilog.Core;
using Serilog.Events;
using Serilog.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serilog.PerformanceTests
{
    [MemoryDiagnoser]
    public class PropertyBinderBenchmark
    {
        private PropertyBinder binder = new PropertyBinder(
                new PropertyValueConverter(10, 1000, 1000, Enumerable.Empty<Type>(), Enumerable.Empty<IDestructuringPolicy>(), false));

        [Benchmark]
        public void CallPositionalConstructProperties()
        {
            var messageTemplate = "Hello {1} {0} nothing more";
            object[] objects = { 0, 1 };
            var mt = new MessageTemplateParser().Parse(messageTemplate);
            binder.ConstructProperties(mt, objects).Select(p => new LogEventProperty(p.Name, p.Value));
        }

        [Benchmark]
        public void CallPositionalConstructPropertiesWithoutExtraArgs()
        {
            var messageTemplate = "Hello {1} {0} nothing more";
            object[] objects = { 0, 1 };
            var mt = new MessageTemplateParser().Parse(messageTemplate);
            binder.ConstructPropertiesWithoutExtraArgs(mt, objects).Select(p => new LogEventProperty(p.Name, p.Value));
        }

        [Benchmark]
        public void CallNamedConstructProperties()
        {
            var messageTemplate = "Hello {who} {what} nothing more";
            object[] objects = { "who", "what" };
            var mt = new MessageTemplateParser().Parse(messageTemplate);
            binder.ConstructProperties(mt, objects).Select(p => new LogEventProperty(p.Name, p.Value));
        }

        [Benchmark]
        public void CallNamedConstructPropertiesWithoutExtraArgs()
        {
            var messageTemplate = "Hello {who} {what} nothing more";
            object[] objects = { "who", "what" };
            var mt = new MessageTemplateParser().Parse(messageTemplate);
            binder.ConstructPropertiesWithoutExtraArgs(mt, objects).Select(p => new LogEventProperty(p.Name, p.Value));
        }
    }
}
