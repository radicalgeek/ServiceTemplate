using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Data.Relational
{
public interface IDataContextResporitory{
        List<SampleEntity> GetAll();
        SampleEntity Get(long id);
        void Post(SampleEntity dataEventRecord );
        void Put(SampleEntity dataEventRecord);
        void Delete(long id);
}
}
