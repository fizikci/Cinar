using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Cinar.Database;
using Cinar.Scripting;
using Cinar.DBTools.Tools;

namespace Cinar.DBTools.CodeGen
{
    public class FileItem : Item
    {
        public bool IsBinary { get; set; }

        public override void Delete()
        {
            File.Delete(this.Path);
            parent.Remove(this);
            Solution.Modified = true;
        }

        public override List<GeneratedCode> GenerateCode(string basePath, List<Table> selectedTables)
        {
            List<GeneratedCode> generatedCodes = new List<GeneratedCode>();

            switch (RepeatGeneration)
            {
                case RepeatGenerationTypes.NoRepeat:
                    generatedCodes.Add(GenerateCode(basePath, selectedTables[0]));
                    break;
                case RepeatGenerationTypes.ForEachCategory:
                    foreach (string cat in selectedTables.Select(t => t.UIMetadata.ModuleName).Distinct())
                        generatedCodes.Add(GenerateCode(basePath, selectedTables.Where(t=>t.UIMetadata.ModuleName==cat).FirstOrDefault()));
                    break;
                case RepeatGenerationTypes.ForEachTable:
                    foreach (Table table in selectedTables)
                        generatedCodes.Add(GenerateCode(basePath, table));
                    break;
            }

            return generatedCodes;
        }
        public GeneratedCode GenerateCode(string basePath, Table table)
        {
            string code = null;

            if (!this.IsBinary)
            {
                Interpreter engineForCode = new Interpreter(File.ReadAllText(this.Path), null);
                engineForCode.SetAttribute("this", this);
                engineForCode.SetAttribute("db", Provider.Database);
                engineForCode.SetAttribute("table", table);
                engineForCode.SetAttribute("util", new Util());
                engineForCode.Parse();
                engineForCode.Execute();

                code = engineForCode.Output;
            }

            string path = Name;
            if (!string.IsNullOrEmpty(this.FileNameTemplate))
            {
                Interpreter engineForPath = new Interpreter(this.FileNameTemplate, null);
                engineForPath.SetAttribute("this", this);
                engineForPath.SetAttribute("db", Provider.Database);
                engineForPath.SetAttribute("table", table);
                engineForPath.SetAttribute("util", new Util());
                engineForPath.Parse();
                engineForPath.Execute();
                path = engineForPath.Output;
            }

            return new GeneratedCode {
                Code = code,
                Path = basePath + "\\" + path
            };
        }

        public override void CreateCode(string basePath, List<Table> selectedTables)
        {
            switch (RepeatGeneration)
            {
                case RepeatGenerationTypes.NoRepeat:
                    CreateCode(basePath, selectedTables[0]);
                    break;
                case RepeatGenerationTypes.ForEachCategory:
                    foreach (string cat in selectedTables.Select(t => t.UIMetadata.ModuleName).Distinct())
                        CreateCode(basePath, selectedTables.Where(t => t.UIMetadata.ModuleName == cat).FirstOrDefault());
                    break;
                case RepeatGenerationTypes.ForEachTable:
                    foreach (Table table in selectedTables)
                        CreateCode(basePath, table);
                    break;
            }
        }
        public void CreateCode(string basePath, Table table)
        {
            GeneratedCode gc = GenerateCode(basePath, table);

            if(this.IsBinary)
                File.Copy(this.Path, gc.Path);
            else
                File.WriteAllText(gc.Path, gc.Code, Encoding.UTF8);
        }

        public FileItem()
        {
            RepeatGeneration = RepeatGenerationTypes.NoRepeat;
        }
    }

    public class GeneratedCode
    {
        public string Path { get; set; }
        public string Code { get; set; }
        public bool IsFolder { get; set; }
    }
}
