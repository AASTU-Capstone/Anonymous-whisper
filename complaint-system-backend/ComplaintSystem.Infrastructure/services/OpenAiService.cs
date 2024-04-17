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
        chat.AppendUserInput("can you extract from the text the following list{Problem Statement, Solution, Financial Projection, Business Model, Market Analysis, Competition} as a key and paragraphs below them as a value and send back as JSON body. if key does not exist return empty value");
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
    public async Task<string> SparkyByQuery(string text, string param, string query)
    {

        Dictionary<string, string> parameterDict = new Dictionary<string, string>
        {
            {"problem","consider the given text is a problem statement for a startup project" },
            {"solution" , "consider the given text is a problem solution for a startup project"},
            {"financial projection", "consider the given text is a financial projection for a startup project" },
            {"buisness model", "consider the given text is a buisness model for a startup project" },
            {"market analysis", "consider the given text is a market analysis for a startup project" },
            {"competitor", "consider the given text is a detailed description of the competitors for a startup project" },
            {"cost to build mvp", "consider the given text is a broken down explanation on the cost to build the MVP for a startup project" },
            {"current cash flow", "consider the given text is a description of the current cash flow for a startup project" }
        };
        var api = new OpenAI_API.OpenAIAPI(Environment.GetEnvironmentVariable("OPENAI_KEY"));
        var chat = api.Chat.CreateConversation();
        chat.RequestParameters.Temperature = 0.4;
        chat.Model = OpenAI_API.Models.Model.ChatGPTTurbo;
        chat.AppendUserInput(parameterDict[param]);
        chat.AppendUserInput(query);
        chat.AppendUserInput(text);
        var result = await chat.GetResponseFromChatbotAsync();
        return result.ToString();
    }
}
