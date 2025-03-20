import './App.css';

function App() {
    class MouseMoveEvent {
        constructor(x, y, t) {
            this.x = x;
            this.y = y;
            this.t = t;
        }
    }

    let mouseMoveEvents = [];
    document.addEventListener("mousemove", handleMouseMove);

    function handleMouseMove(event) {
        mouseMoveEvents.push(new MouseMoveEvent(event.pageX, event.pageY, new Date(Date.now())));
    }

    return (
        <div>
            <button id="sendData" class="button" onClick={handleClick}>Отправить данные</button>
        </div>
    );

    async function handleClick() {
        await fetch('/api/mousemovelogger', {
            method: 'POST',
            headers: {
                'Content-type': 'application/json;charset=UTF-8'
            },
            body: JSON.stringify(mouseMoveEvents)
        })
            .catch((err) => console.log(err.message));
        mouseMoveEvents = [];
    }
}

export default App;