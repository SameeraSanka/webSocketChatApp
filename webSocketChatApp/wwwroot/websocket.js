window.WebSocketClient = {
    connect: function (url, dotNetHelper) {
        var socket = new WebSocket(url);

        socket.onopen = function (event) {
            dotNetHelper.invokeMethodAsync('OnOpen');
        };

        socket.onmessage = function (event) {
            dotNetHelper.invokeMethodAsync('OnMessage', event.data);
        };

        socket.onclose = function (event) {
            dotNetHelper.invokeMethodAsync('OnClose');
        };

        return socket;
    },

    send: function (socket, message) {
        socket.send(message);
    }
};
