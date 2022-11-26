local socket = require("socket")
local server = assert(socket.bind("*", 51515))
local tcp = assert(socket.tcp())
print(socket._VERSION)
print(tcp)

local s = socket.udp()
    s:settimeout(0)
    s:setpeername("74.125.115.104",80)

        local ip, _ = s:getsockname()
        print("ip",ip)

while 1 do

  
  local client = server:accept()
  if client then
    print("werwer")
    local line,ip,port = client:receive()
    print(line,ip,port)
    client:send("it works\n")
  end

end