using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace TodoApp.EntityFrameworkCore;

public abstract class TodoAppEntityFrameworkCoreTestBase : TodoAppTestBase<TodoAppEntityFrameworkCoreTestModule>
{
    protected TodoAppEntityFrameworkCoreTestBase()
    {
    }
}