# Demo for options inheritance with named options in nested options

```json
{
    "MySettings": {
        "SomeValue": "foo1",
        "AnotherValue": "bar1",
        "AnotherMySettings": {
            "SomeValue": "foo2"
        }
    }
}
```

Resolving named configs for `AnotherMySettings` with `mySettings.Get("AnotherMySettings");` returns:
```console
foo2
bar1
```
