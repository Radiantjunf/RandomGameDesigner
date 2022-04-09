import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { games: null, name: '', loading: true };
        this.refreshPage = this.refreshPage.bind(this)

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

    static renderForecastsTable(games) {
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




    render() {
        let contents = this.state.loading
            ? <p></p>
            : Home.renderForecastsTable(this.state.games);



        return (

                <div className="calculator-takeover">
                    <div className="calculator-takeover-contain">
                        <h1 className="header-title">Random Game Designer</h1>
                        <h2 className="header-subtitle">Create a game based on steam your steam Library</h2>
                        <div className="calculator-form">
                            <div className="calculator-takeover-input">
                                <svg version="1.1" width="24" height="24" viewBox="0 0 16 16" className="octicon octicon-search" aria-hidden="true">
                                    <path fillRule="evenodd" d="M11.5 7a4.499 4.499 0 11-8.998 0A4.499 4.499 0 0111.5 7zm-.82 4.74a6 6 0 111.06-1.06l3.04 3.04a.75.75 0 11-1.06 1.06l-3.04-3.04z"></path>
                                </svg>
                            <input type="text" name="player" id="inputQuery" maxLength="80" minLength="2" autoFocus="" v onChange={evt => this.setState({ name: evt.target.value })} placeholder="Your profile url or steamid" required="" aria-label="Profile URL or SteamID"></input>
                            </div>
                            <div className="calculator-takeover-button">
                                <select id="inputCurrency" name="cc" aria-label="Currency">
                                    <option value="all">Genres</option>
                                    <option value="action">Action</option>
                                </select>
                            <button className="btn btn-outline" id="submit-button" onClick={this.refreshPage}>Get Idea</button>
                            </div>
                        </div>                  
                    </div>
                    {contents}
                </div>
                
             );
            }
}