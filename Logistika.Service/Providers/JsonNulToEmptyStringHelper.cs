
namespace Logistika.Service.Providers
{
    //public class JsonNulToEmptyStringHelper : DefaultContractResolver
    //{ 
    //        private readonly char _startingWithChar;
    //        public JsonNulToEmptyStringHelper(char re)
    //        {
    //            _startingWithChar = startingWithChar;
    //        }

    //        protected override IList<JsonProperty> CreateProperties(JsonObjectContract contract)
    //        {
    //            IList<JsonProperty> properties = base.CreateProperties(contract);

    //            // only serializer properties that start with the specified character
    //            properties =
    //              properties.Where(p => p.PropertyName.StartsWith(_startingWithChar.ToString())).ToList();

    //            return properties;
    //        }
    //}

    //public sealed class SubstituteNullWithEmptyStringContractResolver : DefaultContractResolver
    //{
    //    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    //    {
    //        JsonProperty property = base.CreateProperty(member, memberSerialization);

    //        if (property.PropertyType == typeof(string))
    //        {
    //            // Wrap value provider supplied by Json.NET.
    //            property.ValueProvider = new NullToEmptyStringValueProvider(property.ValueProvider);
    //        }

    //        return property;
    //    }

    //    sealed class NullToEmptyStringValueProvider : IValueProvider
    //    {
    //        private readonly IValueProvider Provider;

    //        public NullToEmptyStringValueProvider(IValueProvider provider)
    //        {
    //            if (provider == null) throw new ArgumentNullException("provider");

    //            Provider = provider;
    //        }

    //        public object GetValue(object target)
    //        {
    //            return Provider.GetValue(target) ?? "";
    //        }

    //        public void SetValue(object target, object value)
    //        {
    //            Provider.SetValue(target, value);
    //        }
    //    }
    //}
    /*
    public class NullToEmptyStringResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            return type.GetProperties()
                    .Select(p =>
                    {
                        var jp = base.CreateProperty(p, memberSerialization);
                        jp.ValueProvider = new NullToEmptyStringValueProvider(p);
                        return jp;
                    }).ToList();
        }
    }

    public class NullToEmptyStringValueProvider : IValueProvider
    {
        PropertyInfo _MemberInfo;
        public NullToEmptyStringValueProvider(PropertyInfo memberInfo)
        {
            _MemberInfo = memberInfo;
        }

        public object GetValue(object target)
        {
            object result = _MemberInfo.GetValue(target);
            if (_MemberInfo.PropertyType == typeof(string) && result == null) result = "";
            return result;

        }

        public void SetValue(object target, object value)
        {
            _MemberInfo.SetValue(target, value);
        }
    */

    //public sealed class SubstituteNullWithEmptyStringContractResolver : DefaultContractResolver
    //{
    //    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    //    {
    //        JsonProperty property = base.CreateProperty(member, memberSerialization);

    //        if (property.PropertyType == typeof(string))
    //        {
    //            // Wrap value provider supplied by Json.NET.
    //            property.ValueProvider = new NullToEmptyStringValueProvider(property.ValueProvider);
    //        }

    //        return property;
    //    }

    //    sealed class NullToEmptyStringValueProvider : IValueProvider
    //    {
    //        private readonly IValueProvider Provider;

    //        public NullToEmptyStringValueProvider(IValueProvider provider)
    //        {
    //            if (provider == null) throw new ArgumentNullException("provider");

    //            Provider = provider;
    //        }

    //        public object GetValue(object target)
    //        {
    //            return Provider.GetValue(target) ?? "";
    //        }

    //        public void SetValue(object target, object value)
    //        {
    //            Provider.SetValue(target, value);
    //        }
    //    }
    //}

    //public class NullableValueProvider : IValueProvider
    //{
    //    private readonly object _defaultValue;
    //    private readonly IValueProvider _underlyingValueProvider;


    //    public NullableValueProvider(MemberInfo memberInfo, Type underlyingType)
    //    {
    //        _underlyingValueProvider = new DynamicValueProvider(memberInfo);
    //        _defaultValue = Activator.CreateInstance(underlyingType);
    //    }

    //    public void SetValue(object target, object value)
    //    {
    //        _underlyingValueProvider.SetValue(target, value);
    //    }

    //    public object GetValue(object target)
    //    {
    //        return _underlyingValueProvider.GetValue(target) ?? _defaultValue;
    //    }
    //}

    //public class SpecialContractResolver : DefaultContractResolver
    //{
    //    protected override IValueProvider CreateMemberValueProvider(MemberInfo member)
    //    {
    //        if (member.MemberType == MemberTypes.Property)
    //        {
    //            var pi = (PropertyInfo)member;
    //            if (pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
    //            {
    //                return new NullableValueProvider(member, pi.PropertyType.GetGenericArguments().First());
    //            }
    //        }
    //        else if (member.MemberType == MemberTypes.Field)
    //        {
    //            var fi = (FieldInfo)member;
    //            if (fi.FieldType.IsGenericType && fi.FieldType.GetGenericTypeDefinition() == typeof(Nullable<>))
    //                return new NullableValueProvider(member, fi.FieldType.GetGenericArguments().First());
    //        }

    //        return base.CreateMemberValueProvider(member);
    //    }
    //}
}