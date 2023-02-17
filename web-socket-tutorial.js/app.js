let express = require('express');
let app = express();
let http = require('http').Server(app);

//この文法脳がバグる
const io = require('socket.io')(http);

const PORT = process.env.PORT || 3000;

app.set('view engine', 'ejs');

http.listen(PORT, () => console.log('server listening. Port' + PORT));

app.get('/', (req, res, next) => res.render("index", {}));

io.on('connection', (socket) => {

    //イベントの検知
    socket.on('sendMessage', (msg) => {

        console.log('message :' + msg);

        //イベントの発火
        io.emit('receiveMessage', msg);

    });

});