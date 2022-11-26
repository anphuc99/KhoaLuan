local socket = require("socket")
local host, port = "192.168.1.2", 7070
local tcp = assert(socket.tcp())

tcp:connect(host, port);
tcp:send([[{"name":"pc_test02","msg":"hello ky"}]].."\n");
local s, status, partial = tcp:receive()

print(s)

-- while true do
--     local s, status, partial = tcp:receive()
--     print(s or partial)
--     if status == "closed" then
--       break
--     end
-- end

tcp:close()