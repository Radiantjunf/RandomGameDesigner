import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (

                <div class="calculator-takeover">
                    <div class="calculator-takeover-contain">
                        <h1 class="header-title">Random Game Designer</h1>
                        <h2 class="header-subtitle">Create a game based on steam your steam Library</h2>
                        <form id="profileForm" class="calculator-form" method="GET" action="/calculator/">
                            <div class="calculator-takeover-input">
                                <svg version="1.1" width="24" height="24" viewBox="0 0 16 16" class="octicon octicon-search" aria-hidden="true">
                                    <path fill-rule="evenodd" d="M11.5 7a4.499 4.499 0 11-8.998 0A4.499 4.499 0 0111.5 7zm-.82 4.74a6 6 0 111.06-1.06l3.04 3.04a.75.75 0 11-1.06 1.06l-3.04-3.04z"></path>
                                </svg>
                                <input type="text" name="player" id="inputQuery" maxlength="80" minlength="2" autofocus="" placeholder="Your profile url or steamid" required="" aria-label="Profile URL or SteamID"></input>
                            </div>
                            <div class="calculator-takeover-button">
                                <select id="inputCurrency" name="cc" aria-label="Currency">
                                    <option value="all">Genres</option>
                                    <option value="action">Action</option>
                                </select>
                                <button class="btn btn-outline" id="submit-button">Get Idea</button>
                            </div>
                        </form>                  
                    </div>
                    <div class="flex-container">
                        <div class="game-box">
                            <img class="game-image" src="https://cdn.akamai.steamstatic.com/steam/apps/220/header.jpg?t=1591063154"></img>
                            <h2 class="game-title">Half-Life2</h2>
                            <h2 class="game-text">SinglePlayer</h2>
                            <h2 class="game-text">Action</h2>
                        </div>
                        <div class="game-box">
                            <img class="game-image" src="https://cdn.akamai.steamstatic.com/steam/apps/220/header.jpg?t=1591063154"></img>
                            <h2 class="game-title">Half-Life2</h2>
                            <h2 class="game-text">SinglePlayer</h2>
                            <h2 class="game-text">Action</h2>
                        </div>
                        <div class="game-box">
                            <img class="game-image" src="https://cdn.akamai.steamstatic.com/steam/apps/220/header.jpg?t=1591063154"></img>
                            <h2 class="game-title">Half-Life2</h2>
                            <h2 class="game-text">SinglePlayer</h2>
                            <h2 class="game-text">Action</h2>
                        </div>
                        
                    </div>
                </div>
                
             );
            }
}