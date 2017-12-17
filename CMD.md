## Command

```
dotnet run -c Release --project src/ThailandAdministrativeDivision.Benchmark/ThailandAdministrativeDivision.Benchmark.csproj
```

```
        Method |        Mean |       Error |       StdDev |
-------------- |------------:|------------:|-------------:|
 LoadChangwats |    685.0 us |    10.04 us |     8.385 us |
   LoadAmphoes |  7,357.1 us |   138.86 us |   129.886 us |
   LoadTambons | 73,739.5 us | 1,447.35 us | 1,722.965 us |
```

```
           Method |        Mean |        Error |       StdDev |
----------------- |------------:|-------------:|-------------:|
    LoadProvinces |    692.2 us |     8.329 us |     6.955 us |
    LoadDistricts |  7,695.8 us |    50.436 us |    42.116 us |
 LoadSubdistricts | 76,245.5 us | 1,386.588 us | 1,229.174 us |

```