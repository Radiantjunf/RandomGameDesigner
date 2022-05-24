import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;
    constructor(props) {
        super(props);
        this.state = { games: null, name: '',type: false, loading: true };
        this.refreshPage = this.refreshPage.bind(this)
        this.onKeyUp = this.onKeyUp.bind(this);

    }

    refreshPage() {
        this.populateWeatherData()
    }

    async populateWeatherData() {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ 'name': this.state.name })
        };
        const response = await fetch('game',requestOptions);
        const data = await response.json();
        console.log(this.state.name);
        this.setState({ games: null, loading: true });
        this.setState({ games: data, loading: false });
    }

    static renderForecastsTable(games,type) {
        
        if (type) {
            return (
                <div className="flex-container">
                    {games.map(game =>
                        <div className="game-box" key={game.gameKey}>
                            <h2 className="game-title">{game.topText}</h2>
                            <img className="game-image" src={game.imageLink}></img>
                            <h2 className="game-title">{game.title}</h2>
                            {/*<h2 className="game-text">{game.tags}</h2>*/}
                            <h2 className="game-text">{game.genres}</h2>
                        </div>
                    )}
                </div>

            );
        } else {
            return (
                <div className="flex-container">
                    {games.map(game =>
                        <div className="game-box" key={game.gameKey}>
                            <img className="game-image" src={game.imageLink}></img>
                            <h2 className="game-title">{game.title}</h2>
                            {/*<h2 className="game-text">{game.tags}</h2>*/}
                            <h2 className="game-text">{game.genres}</h2>
                        </div>
                    )}
                </div>

            );
        }
    }

    onKeyUp(event) {
        if (event.charCode === 13) {
            this.refreshPage();
        }
    }




    render() {
        let contents = this.state.loading
            ? <p></p>
            : Home.renderForecastsTable(this.state.games,this.state.type);



        return (
            <div>
                <div className="calculator-takeover">
                    <div className="calculator-takeover-contain">
                        <h1 className="header-title">Random Game Designer</h1>
                        <h2 className="header-subtitle">Create a game based on steam your steam Library</h2>
                        <div className="calculator-form">
                            <div className="calculator-takeover-input">
                                <svg version="1.1" width="24" height="24" viewBox="0 0 16 16" className="octicon octicon-search" aria-hidden="true">
                                    <path fillRule="evenodd" d="M11.5 7a4.499 4.499 0 11-8.998 0A4.499 4.499 0 0111.5 7zm-.82 4.74a6 6 0 111.06-1.06l3.04 3.04a.75.75 0 11-1.06 1.06l-3.04-3.04z"></path>
                                </svg>
                                <input classname= "textBox" type="text" name="player" id="inputQuery" maxLength="80" minLength="2" autoFocus="" v onChange={evt => this.setState({ name: evt.target.value })} onKeyPress={this.onKeyUp} placeholder="Your profile url or steamid" required="" aria-label="Profile URL or SteamID"></input>
                            </div>
                            <div className="calculator-takeover-button">
                                <div className="checkbox">
                                    <input type="checkbox" id="subscribeNews" name="subscribe" value="newsletter" onChange={evt => this.setState({ type: evt.target.checked })}></input>
                                    <label for="subscribeNews">Categories</label>
                                </div>
                                {/*<select id="inputCurrency" name="cc" aria-label="Currency">*/}
                                {/*    <option value="all">Genres</option>*/}
                                {/*    <option value="action">Action</option>*/}
                                {/*</select>*/}
                                <button className="btn btn-outline" id="submit-button" onClick={this.refreshPage}>Get Idea</button>
                            </div>
                        </div>
                        
                    </div>
                    
                </div>
                {contents}
                <div className="container-sm">
                    <div className="divhelp">
                        <h3 class="steamid-examples-header">Examples on what can be entered</h3>
                        <table class="table table-bordered table-responsive-flex steamid-examples">
                            <tbody>
                                <tr>
                                    <td>Steam vanity url</td>
                                    <td>https://steamcommunity.com/id/alexjunf</td>
                                </tr>
                                <tr>
                                    <td>Steam profile url</td>
                                    <td>https://steamcommunity.com/profiles/76561198024878463</td>
                                </tr>                                                           
                                <tr>
                                    <td>64-bit SteamID</td>
                                    <td>76561198024878463</td>
                                </tr>                              
                                <tr>
                                    <td>Steam vanity id</td>
                                    <td>alexjunf</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
                
             );
            }
}