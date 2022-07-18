using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Qognify.QVA.MongoDB.Extensions
{
    public static class IAggregateFluentExtensions
    {
        /// <summary>
        /// MongoDb.Driver lacks a method for "addFields" aggregation.
        /// </summary>
        /// <typeparam name="T">The collection type.</typeparam>
        /// <param name="pipeline">The aggregation pipeline that we are adding new fields to.</param>
        /// <param name="newFieldDefinitions">The list of new field names and values or expressions.</param>
        /// <returns>IAggregateFluent instance for a given collection type that includes the new field(s).</returns>
        public static IAggregateFluent<T> AddFields<T>(this IAggregateFluent<T> pipeline, IEnumerable<(string NewFieldName, object ValueExpression)> newFieldDefinitions)
        {
            if (newFieldDefinitions.Select(d => d.ValueExpression).Any(e => !ValidateExpression(e)))
            {
                throw new ArgumentException("Only strings and value types are supported.");
            }

            var fieldDefinitions = newFieldDefinitions.Aggregate(string.Empty, (result, current) => {
                return $"{(string.IsNullOrEmpty(result) ? result : result + ", ")}'{current.NewFieldName}': {ToRoundTripString(current.ValueExpression)}";
            });
            var addFieldDefinitionDoc = BsonDocument.Parse($"{{ {fieldDefinitions} }}");
            var stageElement = new BsonElement("$addFields", addFieldDefinitionDoc);
            var stage = new BsonDocument(stageElement);
            return pipeline.AppendStage<T>(stage);
        }

        /// <summary>
        /// Validation method
        /// </summary>
        /// <param name="value">An object that can be represented as a primitive type literal or MongoDB "aggregation expression", that is supplied as string.</param>
        /// <returns>False if an argument is not a string or a value type.</returns>
        private static bool ValidateExpression(object value)
        {
            return value switch
            {
                string => true,
                _ when typeof(System.ValueType).IsAssignableFrom(value?.GetType()) => true,
                _ => false
            };
        }

        private static string ToRoundTripString(object value)
        {
            return value switch
            {
                float => $"{value:G9}",
                double => $"{value:G17}",
                bool => $"{value}".ToLower(),
                DateTime dateTimeValue => dateTimeValue.ToString("o", System.Globalization.CultureInfo.InvariantCulture),
                _ => $"{value}"
            };
        }
    }
}