﻿using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace BibleReadings2.Repository.Json
{
    public class JsonReadingsRepository : IReadingsRepository
    {
        private static readonly Assembly _assembly = typeof(JsonReadingsRepository).Assembly;
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public async Task<Day> GetReadings(int month, int day)
        {
            if(month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(month));
            }

            using var stream = GetStream(month);
            var days = await JsonSerializer.DeserializeAsync<Day[]>(stream, _options);

            if(day < 1 || day > days.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(day));
            }

            return days[day - 1];
        }

        private static Stream GetStream(int month)
        {
            return _assembly.GetManifestResourceStream($"BibleReadings2.Repository.Json.data.{month}.json");
        }
    }
}
