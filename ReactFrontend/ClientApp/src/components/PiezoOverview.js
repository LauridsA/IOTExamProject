import React, { Component } from 'react';

export class PiezoOverview extends Component {
    static displayName = PiezoOverview.name;

    constructor(props) {
        super(props);
        this.state = { messages: [], topics: [] };
        this.getMessages = this.getMessages.bind(this);
        this.postMessageSong = this.postMessageSong.bind(this);
        this.postMessageTrack = this.postMessageTrack.bind(this);
        this.postTest = this.postTest.bind(this);
    }

    postMessageSong(e) {
        if (this.inputS.value == "") {
            this.inputS.value = "[Empty]"
        }
        fetch('api/piezo/postMqttMessage/song/' + this.inputS.value, {
            method: 'POST'
        })
    }

    postMessageTrack(e) {
        if (this.inputT.value == "") {
            this.inputT.value = "[Empty]"
        }
        fetch('api/piezo/postMqttMessage/track/' + this.inputT.value, {
            method: 'POST'
        })
    }

    postTest(e) {
        if (this.inputH.value == "") {
            this.inputH.value = "[Empty]"
        }
        fetch('api/piezo/postMqttMessage/test/flicker/' + this.inputH.value, {
            method: 'POST'
        })
    }

    getMessages() {
        fetch('api/piezo/GetMessageReactClient', {
            method: 'GET'
        })
            .then(response => {
                //callback
                response.json().then(data => {
                    var datalistT = [];
                    var datalistM = [];
                    //we implement the logic here.
                    data.forEach(entry => {
                        datalistM = datalistM.concat(entry['message'])
                        datalistT = datalistT.concat(entry['topic'])
                    })
                    this.setState({
                        messages: datalistM,
                        topics: datalistT
                    });
                    console.log(this.state.messages)
                    console.log(this.state.topics)
                })
            })
    }

    render() {
        return (
            <div>
                <h1>Messages</h1>

                <button className="btn btn-primary" onClick={this.getMessages}>Get the messages</button>
                <div>
                    <p> Messages: </p>
                    <ul>
                        {this.state.messages.map(item => {
                            return <li style={{ listStyleType: "none", fontWeight: "bold", fontSize: "17px" }} key={item}> Message: {item}</li>
                        })}
                        {this.state.topics.map(item => {
                            return <li style={{ listStyleType: "none", fontSize: "12px", color: "#999" }} key={item}>With Topic: {item}</li>
                        })}
                    </ul>
                </div>
                <div>Player</div>
                <textarea type="text" placeholder="Play/Stop/Pause/Unpause" ref={(input) => this.inputS = input} /><br />
                <button className="btn btn-primary" onClick={this.postMessageSong}>player</button> <br />
                <div>Track</div> <br />
                <textarea type="text" placeholder="Next/Prev" ref={(input) => this.inputT = input} /> <br />
                <button className="btn btn-primary" onClick={this.postMessageTrack}>song</button> <br />
                <textarea type="text" placeholder="start / int speed" ref={(input) => this.inputH = input} /> <br />
                <button className="btn btn-primary" onClick={this.postTest}>test</button> <br />
            </div>
        );
    }
}
