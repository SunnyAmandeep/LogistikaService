//namespace Logistika.Service.Common.RequestContextHander
//{
//    public class NakedBodyParameterBinding : HttpParameterBinding
//    {
//        public NakedBodyParameterBinding(HttpParameterDescriptor descriptor)
//            : base(descriptor)
//        {

//        }


//        public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider,
//                                                    HttpActionContext actionContext,
//                                                    CancellationToken cancellationToken)
//        {
//            var binding = actionContext
//                .ActionDescriptor
//                .ActionBinding;

//            if (binding.ParameterBindings.Length > 1 ||
//                actionContext.Request.Method == HttpMethod.Get)
//                return EmptyTask.Start();

//            var type = binding
//                        .ParameterBindings[0]
//                        .Descriptor.ParameterType;

//            if (type == typeof(string))
//            {
//                return actionContext.Request.Content
//                        .ReadAsStringAsync()
//                        .ContinueWith((task) =>
//                        {
//                            var stringResult = task.Result;
//                            SetValue(actionContext, stringResult);
//                        });
//            }
//            else if (type == typeof(byte[]))
//            {
//                return actionContext.Request.Content
//                    .ReadAsByteArrayAsync()
//                    .ContinueWith((task) =>
//                    {
//                        byte[] result = task.Result;
//                        SetValue(actionContext, result);
//                    });
//            }

//            throw new InvalidOperationException("Only string and byte[] are supported for [NakedBody] parameters");
//        }

//        public override bool WillReadBody
//        {
//            get
//            {
//                return true;
//            }
//        }
//    }
//}
