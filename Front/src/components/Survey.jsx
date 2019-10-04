import React from "react";
import { Redirect } from "react-router-dom";
import { SurveyOptions } from "./SurveyOptions";
import DeviceOrientation, { Orientation } from 'react-screen-orientation';
import isMobileDevice from "./isMobile";
import "./survey.scss";

const SurveyWeb = (props) => {

    const { location, setOnRight, setOnLeft, voteOnLeft, lastAnswer, voteOnRight, selectedAnswers, leftWasPressed, rightWasPressed } = props;

    if (typeof (location.state) == "undefined")
        return <Redirect to="/"></Redirect>;

    return (
        <section className="hero is-success is-fullheight" tabIndex="0" onKeyDown={(evt) => {
            if (evt.key === "ArrowRight") {
                // Votar no da direita
                setOnRight();
            }
            if (evt.key === "ArrowLeft") {
                // Votar no da esquerda
                setOnLeft();
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
                                model={location.state.model}
                                voteOnLeft={voteOnLeft}
                                lastAnswer={lastAnswer}
                                voteOnRight={voteOnRight}
                                selectedAnswers={selectedAnswers}
                                rightWasPressed={rightWasPressed}
                                leftWasPressed={leftWasPressed}></SurveyOptions>
                        </div>
                    </div>
                </div>
            </div>
        </section>)
}


export class Survey extends React.Component {
    constructor(props) {
        super(props);
        const ua = navigator.userAgent || navigator.vendor || window.opera;

        this.state = {
            selectedAnswers: [],
            lastAnswer: null,
            rightWasPressed: false,
            leftWasPressed: false,
            isMobile: isMobileDevice(),
            isInstagram: (ua.indexOf('Instagram') > -1) ? true : false,
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
        if (this.state.isInstagram) {
            return <div className="hero-body" style={{ background: "white" }}>
                <div className="container has-text-centered">
                    <h1>Aparentemente, você está no Instagram. <span role="img" aria-label="Girar">🤳</span></h1>
                    <h2>Por favor, acesso <a href="http://pesquisapsicologia.dframos.com">pesquisapsicologia.dframos.com no seu navegador</a></h2>
                </div>
            </div>
        }

        if (this.state.isMobile) {
            return (
                <DeviceOrientation lockOrientation={'landscape'}>
                    <Orientation orientation='landscape' alwaysRender={false}>
                        <div style={{ background: "#9B3134", height: "100vh", overflow: "hidden" }}>
                            <SurveyOptions
                                model={this.props.location.state.model}
                                voteOnLeft={this.voteOnLeft}
                                lastAnswer={this.state.lastAnswer}
                                voteOnRight={this.voteOnRight}
                                selectedAnswers={this.state.selectedAnswers}
                                rightWasPressed={this.state.rightWasPressed}
                                isMobile={this.state.isMobile}
                                leftWasPressed={this.state.leftWasPressed}></SurveyOptions>
                        </div>
                    </Orientation>
                    <Orientation orientation='portrait' alwaysRender={false}>
                        <div className="hero-body" style={{ background: "white" }}>
                            <div className="container has-text-centered">
                                <h1>Por favor gire <span role="img" aria-label="Girar">🔄</span></h1>
                                <h1>seu celular para que possamos iniciar</h1>
                            </div>
                        </div>
                    </Orientation>
                </DeviceOrientation>
            )
        } else {
            return <SurveyWeb {...this.props} {...this.state} setOnRight={this.setOnRight}
                setOnLeft={this.setOnLeft} voteOnLeft={this.voteOnLeft} voteOnRight={this.voteOnRight} />
        };
    }
}
