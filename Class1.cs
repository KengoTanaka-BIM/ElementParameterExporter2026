using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Text;

namespace ElementParameterExporter2026
{
    //Revit2026用にTransaction属性を指定
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,ref string message,ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            //ユーザーが選択した要素を取得
            ICollection<ElementId> selectedIds = uidoc.Selection.GetElementIds();
            if (selectedIds.Count ==0)
            {
                TaskDialog.Show("パラメータ出力", "要素を選択してください。");
                return Result.Cancelled;

            }
            //ファイル保存パスの指定(ドキュメントと同じフォルダ)
            string docPath = doc.PathName;
            string exportDir = string.IsNullOrEmpty(docPath) ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) : Path.GetDirectoryName(docPath);
            string csvPath = Path.Combine(exportDir, "SelectedElements_Parameters.csv");

            var sb = new StringBuilder();
            sb.AppendLine("ElementId,ParameterName,ParameterValue");

            //各要素についてパラメータを取得
            foreach(ElementId id in selectedIds)
            {
                Element elem = doc.GetElement(id);
                foreach(Parameter param in elem.Parameters)
                {
                    string paramName = param.Definition.Name;
                    string paramValue = GetParameterValue(param);
                    sb.AppendLine($"{elem.Id},{Escape(paramName)},{Escape(paramValue)}");

                }
            }
            //SCVファイルとして保存
            File.WriteAllText(csvPath, sb.ToString(), Encoding.UTF8);
            TaskDialog.Show("パラメータ出力", $"csvを保存しました:\n{csvPath}");

            return Result.Succeeded;

        }
        //パラメータの値を文字列として取得
        private string GetParameterValue(Parameter param)
        {
            switch (param.StorageType)
            {
                case StorageType.Double:
                    return param.AsDouble().ToString("F2");
                case StorageType.ElementId:
                    return param.AsElementId().ToString();  // 修正: IntegerValueは無く、ToString()で取得
                case StorageType.Integer:
                    return param.AsInteger().ToString();
                case StorageType.String:
                    return param.AsString();
                default:
                    return "";


            }
        }
        //csv用に文字列をエスケープ(カンマ、改行、"など)
        private string Escape(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            if (s.Contains(",") || s.Contains("\"") || s.Contains("\n"))
            {
                return $"\"{s.Replace("\"", "\"\"")}\"";
            }
            return s;
        }
    }         
}