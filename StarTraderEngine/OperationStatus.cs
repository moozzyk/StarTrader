namespace StarTrader
{
    public class OperationStatus
    {
        private readonly bool m_success;
        private readonly string m_detail;

        public OperationStatus(bool success)
        {
            m_success = success;
        }

        public OperationStatus(bool success, string detail)
            : this(success)
        {
            // TODO - make it a string resource
            m_detail = detail;
        }

        public string Detail
        {
            get { return m_detail; }
        }

        public static implicit operator bool(OperationStatus status)
        {
            return status.m_success;
        }

        public static implicit operator OperationStatus(bool status)
        {
            return new OperationStatus(status);
        }
    }
}
