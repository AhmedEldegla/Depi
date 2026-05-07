using DEPI.Application.Interfaces;
using DEPI.Application.Repositories.AIMatching;
using DEPI.Domain.Entities.AIMatching;

namespace DEPI.Application.Services.AIMatching;

public class AIModelConfigService : IAIModelConfigService
{
    private readonly IAIModelConfigRepository _configRepository;
    private readonly IAILogRepository _logRepository;

    public AIModelConfigService(
        IAIModelConfigRepository configRepository,
        IAILogRepository logRepository)
    {
        _configRepository = configRepository;
        _logRepository = logRepository;
    }

    public async Task<AIModelConfigResult> GetActiveConfigurationAsync()
    {
        var config = await _configRepository.GetDefaultAsync();
        
        if (config == null)
        {
            config = await CreateDefaultConfigurationAsync();
        }
        
        return MapToResult(config);
    }

    public async Task<AIModelConfigResult> UpdateConfigurationAsync(AIModelConfigUpdate update)
    {
        var config = await _configRepository.GetDefaultAsync();
        
        if (config == null)
        {
            config = await CreateDefaultConfigurationAsync();
        }

        if (update.Temperature.HasValue)
            config.Temperature = update.Temperature.Value;
            
        if (update.MaxTokens.HasValue)
            config.MaxTokens = update.MaxTokens.Value;
            
        if (update.MatchThreshold.HasValue)
            config.MatchThreshold = update.MatchThreshold.Value;
            
        if (update.IsActive.HasValue)
            config.IsActive = update.IsActive.Value;
            
        await _configRepository.UpdateAsync(config);
        
        return MapToResult(config);
    }

    public async Task<decimal> GetMatchThresholdAsync()
    {
        var config = await _configRepository.GetDefaultAsync();
        return config?.MatchThreshold ?? 0.7m;
    }

    public async Task<bool> ValidateConfigurationAsync()
    {
        var config = await _configRepository.GetDefaultAsync();
        
        if (config == null) return false;
        
        if (config.Temperature < 0 || config.Temperature > 2) return false;
        if (config.MaxTokens < 100 || config.MaxTokens > 100000) return false;
        if (config.MatchThreshold < 0 || config.MatchThreshold > 1) return false;
        if (string.IsNullOrEmpty(config.ModelId)) return false;
        
        return true;
    }

    private async Task<AIModelConfig> CreateDefaultConfigurationAsync()
    {
        var defaultConfig = new AIModelConfig
        {
            Name = "Default AI Configuration",
            Provider = "OpenAI",
            ModelId = "gpt-4",
            Endpoint = "https://api.openai.com/v1",
            Temperature = 0.7m,
            MaxTokens = 2000,
            MatchThreshold = 0.7m,
            MinConfidenceScore = 0.5m,
            IsActive = true,
            IsDefault = true,
            MaxRetries = 3,
            TimeoutSeconds = 30
        };

        await _configRepository.AddAsync(defaultConfig);
        
        return defaultConfig;
    }

    private AIModelConfigResult MapToResult(AIModelConfig config)
    {
        return new AIModelConfigResult
        {
            Id = config.Id,
            Name = config.Name,
            Provider = config.Provider,
            ModelId = config.ModelId,
            Temperature = config.Temperature,
            MaxTokens = config.MaxTokens,
            MatchThreshold = config.MatchThreshold,
            IsActive = config.IsActive
        };
    }
}