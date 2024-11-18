using Microsoft.Extensions.Logging;
using System;

namespace DataGridAvto.AvtoManager
{
    static internal class LoggingHelper
    {
        private const string InfoLoggerTemplateAvto =
           "Заполнено {0} для авто с идентификатором {1}, прошло время: {3} мс; дата: {4}";
        private const string ErrorLoggerTemplateAvto =
            "Не удалось заполнить {0} для авто с идентификатором {1}, прошло время: {3} мс; дата: {4}; сообщение об ошибке: {5}";
        private const string ErrorLoggerTemplateCommon =
            "Не удалось завершить {0}, дата: {1}; сообщение об ошибке: {2}";

        /// <summary>
        /// Залогировать информацию о действии с авто
        /// </summary>
        public static void LogErrorAvto(ILogger logger, string actionName, Guid carId, long msElapsed, string errorMessage, string carName = null)
        {
            logger.LogError(
                string.Format(
                            ErrorLoggerTemplateAvto,
                            actionName,
                            carId,
                            carName ?? "-",
                            msElapsed,
                            DateTime.Now,
                            errorMessage
                            )
                );
        }

        /// <summary>
        /// Логирование ошибки при действии с авто
        /// </summary>
        public static void LogInfoCar(ILogger logger, string actionName, Guid carId, long msElapsed, string carName = null)
        {
            logger.LogInformation(
                string.Format(
                              InfoLoggerTemplateAvto,
                              actionName,
                              carId,
                              carName ?? "-",
                              msElapsed,
                              DateTime.Now
                              )
                );
        }

        /// <summary>
        /// Логирование ошибки
        /// </summary>
        public static void LogError(ILogger logger, string actionName, string errorMessage)
        {
            logger.LogError(string.Format(
                                          ErrorLoggerTemplateCommon,
                                          actionName,
                                          DateTime.Now,
                                          errorMessage
                                          )
                );
        }
    }
}