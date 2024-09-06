using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microwave_benner.Application.UseCases
{
    public interface IResumeHeatingTaskUseCase
    {
        Task Execute(int id);
    }

}
