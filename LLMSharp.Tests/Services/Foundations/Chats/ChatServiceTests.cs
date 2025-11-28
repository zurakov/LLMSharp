// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using LLMSharp.Brokers.ModelEngines;
using LLMSharp.Brokers.Times;
using LLMSharp.Models.Chats;
using LLMSharp.Models.ModelEngines;
using LLMSharp.Services.Foundations.Chats;
using Moq;
using Xunit;

namespace LLMSharp.Tests.Services.Foundations.Chats
{
    public class ChatServiceTests
    {
        private readonly Mock<IModelEngineBroker> modelEngineBrokerMock;
        private readonly Mock<ITimeBroker> timeBrokerMock;
        private readonly IChatService chatService;

        public ChatServiceTests()
        {
            this.modelEngineBrokerMock = new Mock<IModelEngineBroker>();
            this.timeBrokerMock = new Mock<ITimeBroker>();

            this.chatService = new ChatService(
                this.modelEngineBrokerMock.Object,
                this.timeBrokerMock.Object);
        }

        [Fact]
        public async Task ShouldGenerateChatResponseAsync()
        {
            // given
            string inputPrompt = "Hello";
            string expectedContent = "Generated response for: Hello";
            int expectedTokenCount = 4;

            var model = new ModelInstance
            {
                Id = Guid.NewGuid(),
                ModelPath = "test-model.gguf",
                IsLoaded = true
            };

            var request = new ChatRequest
            {
                Prompt = inputPrompt,
                MaxTokens = 100,
                Temperature = 0.7
            };

            DateTimeOffset startTime = DateTimeOffset.UtcNow;
            DateTimeOffset endTime = startTime.AddMilliseconds(100);

            this.timeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                .Returns(startTime);

            this.modelEngineBrokerMock.Setup(broker =>
                broker.GenerateTextAsync(
                    model,
                    inputPrompt,
                    It.IsAny<GenerationOptions>()))
                .ReturnsAsync(expectedContent);

            this.modelEngineBrokerMock.Setup(broker =>
                broker.GetTokenCountAsync(model, expectedContent))
                .ReturnsAsync(expectedTokenCount);

            // when
            ChatResponse actualResponse = await this.chatService.ChatAsync(model, request);

            // then
            actualResponse.Should().NotBeNull();
            actualResponse.Content.Should().Be(expectedContent);
            actualResponse.TokenCount.Should().Be(expectedTokenCount);
            actualResponse.ModelPath.Should().Be(model.ModelPath);

            this.timeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                Times.AtLeastOnce);

            this.modelEngineBrokerMock.Verify(broker =>
                broker.GenerateTextAsync(
                    model,
                    inputPrompt,
                    It.IsAny<GenerationOptions>()),
                Times.Once);

            this.modelEngineBrokerMock.Verify(broker =>
                broker.GetTokenCountAsync(model, expectedContent),
                Times.Once);
        }
    }
}
