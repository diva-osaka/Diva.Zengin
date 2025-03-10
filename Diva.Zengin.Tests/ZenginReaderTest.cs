using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Diva.Zengin.Formats;
using JetBrains.Annotations;
using Xunit;

namespace Diva.Zengin.Tests;

/// <summary>
/// ZenginReader のテストクラス。
/// </summary>
[TestSubject(typeof(ZenginReader<>))]
public class ZenginReaderTest
{
    /// <summary>
    /// 結果がない場合、TにレコードclassのList, Arrayを指定していた場合は空のコレクションを返す。
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    [Theory]
    [InlineData(typeof(List<振込入金通知A>))]
    [InlineData(typeof(振込入金通知A[]))]
    [InlineData(typeof(List<入出金取引明細1>))]
    [InlineData(typeof(入出金取引明細1[]))]
    [InlineData(typeof(List<総合振込>))]
    [InlineData(typeof(総合振込[]))]
    public Task ReadAsync_CollectionType_ReturnsEmpty(Type type)
    {
        var readerType = typeof(ZenginReader<>).MakeGenericType(type);
        var reader = Activator.CreateInstance(readerType, new MemoryStream());
        Assert.NotNull(reader);

        var readAsyncMethod = readerType.GetMethod(nameof(ZenginReader<object>.ReadAsync));
        Assert.NotNull(readAsyncMethod);
        
        var task = readAsyncMethod.Invoke(reader, [FileFormat.Csv]);
        Assert.NotNull(task);
        
        // Get the awaiter via reflection
        var awaitMethod = task.GetType().GetMethod(nameof(Task.GetAwaiter));
        Assert.NotNull(awaitMethod);
        
        var awaiter = awaitMethod.Invoke(task, null);
        Assert.NotNull(awaiter);

        var resultMethod = awaiter.GetType().GetMethod("GetResult");
        Assert.NotNull(resultMethod);
        
        var result = resultMethod.Invoke(awaiter, null);

        // Check that it's a collection and it's empty
        Assert.NotNull(result);
        var count = ((ICollection)result).Count;
        Assert.Equal(0, count);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 結果がない場合、Tにレコードclassを指定していた場合はnullを返す。
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    [Theory]
    [InlineData(typeof(振込入金通知A))]
    [InlineData(typeof(入出金取引明細1))]
    [InlineData(typeof(総合振込))]
    public Task ReadAsync_SingleType_ReturnsNull(Type type)
    {
        var readerType = typeof(ZenginReader<>).MakeGenericType(type);
        var reader = Activator.CreateInstance(readerType, new MemoryStream());
        Assert.NotNull(reader);

        var readAsyncMethod = readerType.GetMethod(nameof(ZenginReader<object>.ReadAsync));
        Assert.NotNull(readAsyncMethod);
        
        var task = readAsyncMethod.Invoke(reader, [FileFormat.Csv]);
        Assert.NotNull(task);
        
        // Get the awaiter via reflection
        var awaitMethod = task.GetType().GetMethod(nameof(Task.GetAwaiter));
        Assert.NotNull(awaitMethod);
        
        var awaiter = awaitMethod.Invoke(task, null);
        Assert.NotNull(awaiter);

        var resultMethod = awaiter.GetType().GetMethod("GetResult");
        Assert.NotNull(resultMethod);
        
        var result = resultMethod.Invoke(awaiter, null);

        Assert.Null(result);
        return Task.CompletedTask;
    }
}