namespace PatientManagerServices.Extras;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}