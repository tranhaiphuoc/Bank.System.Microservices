﻿using Dapper;
using System.Data;

namespace Bank.Transaction.Infrastructure.TypeHandlers;

internal class MySqlGuidTypeHandler : SqlMapper.TypeHandler<Guid>
{
    public override void SetValue(IDbDataParameter parameter, Guid guid)
    {
        parameter.Value = guid.ToString();
    }

    public override Guid Parse(object value)
    {
        return new Guid(value.ToString()!);
    }
}
