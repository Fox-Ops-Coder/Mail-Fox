namespace MailFox.UI.Login.Services
{
    internal sealed class ServiceAdapter
    {
        private const string MailRu = "Mail.ru";
        private const string Google = "Google";
        private const string Outlook = "Outlook";

        private readonly ServiceType serviceType;
        private readonly string typeName;

        public ServiceType ServiceType => serviceType;
        public string TypeName => typeName;

        public ServiceAdapter(ServiceType serviceType)
        {
            this.serviceType = serviceType;

            typeName = serviceType switch
            {
                ServiceType.MailRu => MailRu,
                ServiceType.Google => Google,
                ServiceType.Outlook => Outlook,
                _ => string.Empty,
            };
        }

        public override string ToString() => typeName;
    }
}