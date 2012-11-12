﻿#include "DependencyObjectEx.h"

using namespace WCFArchitect::Toolkit::WinRT;
using namespace Platform;
using namespace Windows::UI::Xaml;
using namespace Windows::UI::Core;

DependencyObjectEx::DependencyObjectEx()
{
}

Object^ DependencyObjectEx::GetValueThreaded(DependencyProperty^ dp)
{
	if (Window::Current->Dispatcher->HasThreadAccess) return GetValue(dp);
	
	Object^ v;
	DispatchedHandler^ lamba = ref new DispatchedHandler([v, dp, this]() mutable { v = GetValue(dp); });
	Window::Current->Dispatcher->RunAsync(CoreDispatcherPriority::Normal, lamba);
}

void DependencyObjectEx::SetValueThreaded(DependencyProperty^ dp, Object^ value)
{
	if (Window::Current->Dispatcher->HasThreadAccess) 
	{
		SetValue(dp, value);
		return;
	}
	DispatchedHandler^ lamba = ref new DispatchedHandler([value, dp, this]() { SetValue(dp, value); });
	Window::Current->Dispatcher->RunAsync(CoreDispatcherPriority::Normal, lamba);
}