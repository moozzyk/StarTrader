namespace StarTrader
{
    // TODO - this should probably be an exception class
    public class OperationStatus<T>
    {
        private readonly T m_success;
        private readonly string m_detail;

        public OperationStatus(T success)
        {
            m_success = success;
        }

        public OperationStatus(T success, string detail)
            : this(success)
        {
            // TODO - make it a string resource
            m_detail = detail;
        }

        public string Detail
        {
            get { return m_detail; }
        }

        public static implicit operator T(OperationStatus<T> status)
        {
            return status.m_success;
        }

        public static implicit operator OperationStatus<T>(T status)
        {
            return new OperationStatus<T>(status);
        }
    }
}
