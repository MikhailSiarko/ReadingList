using System.Linq;
using System.Reflection;
using System.Text;

namespace ReadingList.Domain.Services.Validation
{
    public class OnNotExistExceptionData
    {
        public string EntityTypeName { get; }

        public string Params
        {
            get
            {
                var stringBuilder = _messageParams.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Select(prop => $"{prop.Name}: {prop.GetValue(_messageParams)}").Aggregate(new StringBuilder(),
                        (builder, s) => builder.AppendFormat("{0}, ", s));

                return stringBuilder.ToString(0, stringBuilder.Length - 2);
            }
        }

        private readonly object _messageParams;

        public OnNotExistExceptionData(string entityTypeName, object messageParams)
        {
            EntityTypeName = entityTypeName;
            _messageParams = messageParams;
        }
    }
}