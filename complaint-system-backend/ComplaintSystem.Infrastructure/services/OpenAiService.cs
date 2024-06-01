using Microsoft.Extensions.Options;
using  ComplaintSystem.Application.Persistence.Contracts.Common;
using  ComplaintSystem.Infrastructure.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace   ComplaintSystem.Infrastructure.Services;

public class OpenAiService : IOpenAiServices
{
    private readonly OpenAi _openAi;
    public OpenAiService(IOptions<OpenAi> options)
    {
        _openAi = options.Value;
    }
    public async Task<string> ExtractPdfToJson(string texts)
    {
        var api = new OpenAI_API.OpenAIAPI(Environment.GetEnvironmentVariable("OPENAI_KEY"));
        var chat = api.Chat.CreateConversation();
        chat.RequestParameters.Temperature = 0.2;
        chat.Model = OpenAI_API.Models.Model.ChatGPTTurbo;
       chat.AppendUserInput(texts);
        var result = await chat.GetResponseFromChatbotAsync();
        return result.ToString();
    }

    public async Task<string> MakeitProffessional(string text)
    {
        var api = new OpenAI_API.OpenAIAPI(Environment.GetEnvironmentVariable("OPENAI_KEY"));
        var chat = api.Chat.CreateConversation();
        chat.RequestParameters.Temperature = 0.4;
        chat.Model = OpenAI_API.Models.Model.ChatGPTTurbo;
        chat.AppendUserInput("The goal is to make the given paragraphs more aestheticaly pleasant and professional. I want it into two keys. first is a suggested where part of sentences of the paragraphs that need  to be changed for improvement and the reason as {text: reason:} in JSON array literal format . The second is modified part where you populate it with the actual modified paragraph as a single value. return the response as JSON using the following format {Suggestion, Modified}.");
        chat.AppendUserInput(text);
        var result = await chat.GetResponseFromChatbotAsync();
        return result.ToString();
    }

    public async Task<string> MessageChat(string message)
    {
        var api = new OpenAI_API.OpenAIAPI(Environment.GetEnvironmentVariable("OPENAI_KEY"));
        var chat = api.Chat.CreateConversation();
        chat.RequestParameters.Temperature = 0.4;
        chat.Model = OpenAI_API.Models.Model.ChatGPTTurbo;
        chat.AppendSystemMessage("Consider the message is from user using a chat bot of anti corruption commision management system where users enter thier specific case as a complaint. the system offers users to enter a complaint with the attributes including images, audios, documents and content explaining the case at hand. The system has actors including adminstrator, managers and subordinates to handle complaint cases submitted by the user of the system. return a message for the attached message below considering the management system mentioned before");
        //chat.AppendExampleChatbotOutput
        chat.AppendUserInput(message);
        var result = await chat.GetResponseFromChatbotAsync();
        return result.ToString();

    }
}
