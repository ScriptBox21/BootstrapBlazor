﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 自动生成客户端 ID 组件基类
    /// </summary>
    public abstract class IdComponentBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 IJSRuntime 实例
        /// </summary>
        [Inject] protected IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            // 客户端组件生成后通过 invoke 生成客户端组件 id
            if (firstRender)
            {
                if (string.IsNullOrEmpty(Id))
                {
                    // 生成 Id
                    Id = await JSRuntime.GetClientIdAsync();
                    await AfterGenerateIdAsync();
                    await InvokeAsync(StateHasChanged).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// 客户端 Id 生成后调用此方法
        /// </summary>
        /// <returns></returns>
        protected virtual Task AfterGenerateIdAsync() => Task.CompletedTask;
    }
}