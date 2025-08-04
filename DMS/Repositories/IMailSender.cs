namespace DMS.Repositories
{
    public interface IMailSender
    {
        Task<bool> EmailSendAsync(string email, string Subject, string message);
        Task<bool> EmailwithAttachmentSendAsync(string email, string subject, string message, byte[] attachment, string attachmentName);
    }
}
