// Copyright (c) Snapshot Games 2014, All Rights Reserved, http://www.snapshotgames.com

using System;

/// <summary>
/// Attribute which marks class field to be validated for assigned value
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class LinkableAsset : Attribute
{
}
