from subprocess import Popen
camunda = Popen("start-camunda.bat", shell=True,
                cwd="./CamundaClient")
camunda.wait()
dotnet = Popen("dotnet run", shell=True, cwd="./DAR.API")
vue = Popen("npm run serve", shell=True,
            cwd="./DAR.SPA/client")
dotnet.wait()
vue.wait()
