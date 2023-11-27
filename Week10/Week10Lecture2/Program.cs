using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Communication.Email;


namespace Week10Lecture2;
internal class Program
{
    static async Task Main(string[] args)
    {
        // this serviceConnectionString is stored in the code diectly in this example for demo purpose
        // it should be stored in the server when working for a business application.
        // ref: endpoint=https://jhankecommservice.unitedstates.communication.azure.com/;accesskey=EcVPY6xJrHg7QMV2+oLtc/KgKp+17ZTlRK10h30ahkXfCmQcOoj3+r4gAuieFwpR/4ekmTafHrJ5W5XvoZJK5g==
        var sender = "DoNotReply@99e64364-25d6-4d64-ba95-3a87ff4bbdb2.azurecomm.net";
        string serviceConnectionString =  "endpoint=https://jhankecommservice.unitedstates.communication.azure.com/;accesskey=EcVPY6xJrHg7QMV2+oLtc/KgKp+17ZTlRK10h30ahkXfCmQcOoj3+r4gAuieFwpR/4ekmTafHrJ5W5XvoZJK5g==";
        
        EmailClient emailClient = new EmailClient(serviceConnectionString);
        var subject = "Hello CIDM4360";
        var htmlContent = @"
                        <html>
                            <body>
                                <h1 style=color:red>Testing Email for Azure Email Service</h1>
                                <h4>This is a HTML content</h4>
                                <p>Happy Learning!!</p>
                            </body>
                        </html>";

        Console.WriteLine("Please input recipient email address: ");
        string? recipient = Console.ReadLine();

        Console.WriteLine("Sending email with Async no Wait...");
            EmailSendOperation emailSendOperation = await emailClient.SendAsync(
            Azure.WaitUntil.Started,
            sender,
            recipient,
            subject,
            htmlContent);

        /// Call UpdateStatus on the email send operation to poll for the status manually.
        try
        {
            while (true)
            {
                await emailSendOperation.UpdateStatusAsync();
                if (emailSendOperation.HasCompleted)
                {
                    break;
                }
                await Task.Delay(2000);
            }

            if (emailSendOperation.HasValue)
            {
                Console.WriteLine($"Email queued for delivery. Status = {emailSendOperation.Value.Status}");
            }
        }
        catch (RequestFailedException ex)
        {
            Console.WriteLine($"Email send failed with Code = {ex.ErrorCode} and Message = {ex.Message}");
        }

        /// Get the OperationId so that it can be used for tracking the message for troubleshooting
        string operationId = emailSendOperation.Id;
        Console.WriteLine($"Email operation id = {operationId}");
    }    
}