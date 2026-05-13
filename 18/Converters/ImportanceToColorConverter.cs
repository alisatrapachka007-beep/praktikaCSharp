using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Day15.Converters
{
    /// <summary>
    /// Конвертер для подсветки важных диагнозов (красный/жёлтый)
    /// </summary>
    public class ImportanceToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string diagnosis = value as string;
            if (string.IsNullOrEmpty(diagnosis))
                return new SolidColorBrush(Colors.Black);

            // Критические диагнозы - красный
            string[] criticalDiagnoses = { "инфаркт", "инсульт", "онкология", "рак", "критическое" };
            foreach (var crit in criticalDiagnoses)
            {
                if (diagnosis.ToLower().Contains(crit))
                    return new SolidColorBrush(Colors.Red);
            }

            // Важные диагнозы - оранжевый/желтый
            string[] importantDiagnoses = { "гипертония", "диабет", "астма", "эпилепсия", "хронический" };
            foreach (var imp in importantDiagnoses)
            {
                if (diagnosis.ToLower().Contains(imp))
                    return new SolidColorBrush(Color.FromRgb(255, 140, 0)); // Оранжевый
            }

            // Обычные - черный
            return new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}