import React from "react";
import { Redirect } from "react-router-dom";
import { SurveyOptions } from "./SurveyOptions";
export class Survey extends React.Component {
    constructor(props) {
        super(props);
        require("./survey.scss");
        this.state = {
            selectedAnswers: [],
            lastAnswer: null,
            rightWasPressed: false,
            leftWasPressed: false
        };
        this.voteOnLeft = this.voteOnLeft.bind(this);
        this.voteOnRight = this.voteOnRight.bind(this);
        this.setOnRight = this.setOnRight.bind(this);
        this.setOnLeft = this.setOnLeft.bind(this);
    }
    voteOnLeft(valueAnswerId, interVal, answerId) {
        const { selectedAnswers } = this.state;
        selectedAnswers.push({
            valueAnswerId: valueAnswerId,
            interVal: interVal
        });
        this.setState({ rightWasPressed: false, leftWasPressed: false, selectedAnswers, lastAnswer: answerId });
    }
    voteOnRight(valueAnswerId, interVal, answerId) {
        const { selectedAnswers } = this.state;
        selectedAnswers.push({
            valueAnswerId: valueAnswerId,
            interVal: interVal,
        });
        this.setState({ rightWasPressed: false, leftWasPressed: false, selectedAnswers, lastAnswer: answerId });
    }
    setOnRight() {
        this.setState({ rightWasPressed: true });
    }
    setOnLeft() {
        this.setState({ leftWasPressed: true });
    }
    render() {
        if (typeof (this.props.location.state) == "undefined")
            return <Redirect to="/"></Redirect>;
        return (<section className="hero is-success is-fullheight" tabIndex="0" onKeyDown={(evt) => {
            if (evt.key === "ArrowRight") {
                // Votar no da direita
                this.setOnRight();
            }
            if (evt.key === "ArrowLeft") {
                // Votar no da esquerda
                this.setOnLeft();
            }
        }}>
            <div className="hero-head">
                <header className="navbar">
                    <div className="container">
                        <div className="navbar-brand">
                            <span className="navbar-burger burger">
                                <span></span>
                                <span></span>
                                <span></span>
                            </span>
                        </div>
                        <div id="navbarMenuHeroC" className="navbar-menu">
                            <div className="navbar-end">
                                <a className="navbar-item" href="mailto:">
                                    Dúvidas? Entre em contato
                      </a>
                                <span className="navbar-item">
                                    <a href="http://github.com/kkrico" target="_blank" rel="noopener noreferrer" className="button is-success is-inverted">
                                        <span className="icon">
                                            <i className="fab fa-github"></i>
                                        </span>
                                        <span>Made by Daniel</span>
                                    </a>
                                </span>
                            </div>
                        </div>
                    </div>
                </header>
            </div>

            <div className="hero-body">
                <div className="container has-text-centered">
                    <div className="column is-8 is-offset-2">
                        <div className="center-text">
                            <SurveyOptions
                                model={this.props.location.state.model}
                                voteOnLeft={this.voteOnLeft}
                                lastAnswer={this.state.lastAnswer}
                                voteOnRight={this.voteOnRight}
                                selectedAnswers={this.state.selectedAnswers}
                                rightWasPressed={this.state.rightWasPressed}
                                leftWasPressed={this.state.leftWasPressed}></SurveyOptions>
                        </div>
                    </div>
                </div>
            </div>
        </section>);
    }
}