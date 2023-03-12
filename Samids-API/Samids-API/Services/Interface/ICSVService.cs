using CsvHelper.Configuration;
using Samids_API.Models;

namespace Samids_API.Services.Interface
{
    public interface ICSVService
    {
        //Student
        Task<CRUDReturn> ReadStudentsFromCsv(string filePath);

        //Subject
        Task<CRUDReturn> ReadSubjectsFromCsv(string filePath);
        //Faculty
        Task<CRUDReturn> ReadFacultiesFromCsv(string filePath);



    }
}
