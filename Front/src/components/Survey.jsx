import React from "react";
import { Redirect } from "react-router-dom";

const calculateCssClass = (shown) => {
    let css = "column";
    debugger;
    if (!shown) {
        css += " hidden";
    }

    return css;
}

const SurveyOptions = ({ model }) => {

    return <React.Fragment>
        <p className="title white shadow is-1">{model.surveyCommand}</p>
        <br></br>
        {
            model.questions.map((question, index) => <div className="columns">
                {
                    question.options.map(option => {
                        const imageBase64 = "data:image/png;base64, " + option.base64Photo;
                        debugger;
                        return <div className={calculateCssClass(index === 0)}>
                            <img src={imageBase64} className="userphoto animated grow" onClick={() => { window.alert(option.photoId) }}></img>
                            <br></br>
                            <button className="button" onClick={() => { window.alert(option.photoId) }}>Candidato</button>
                        </div>
                    })
                }
            </div>)
        }

    </React.Fragment>
}

export class Survey extends React.Component {
    constructor(props) {
        super(props);

        require("./survey.scss")
    }
    render() {

        if (typeof (this.props.location.state) == "undefined")
            return <Redirect to="/"></Redirect>
        return (
            <section className="hero is-success is-fullheight" tabIndex="0" onKeyDown={(evt) => {
                if (evt.key === "ArrowRight") {
                    // Votar no da direita
                    window.alert("Direita")
                }
                if (evt.key === "ArrowLeft") {
                    // Votar no da esquerda
                    window.alert("Esquerda")
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
                            <div id="navbarMenuHeroC" className="navbar-menu is-active">
                                <div className="navbar-end">
                                    <a className="navbar-item" href="/">
                                        Voltar ao inicio
                      </a>
                                    <a className="navbar-item is-active" href="/pesquisa">
                                        Questionário
                      </a>
                                    <a className="navbar-item" href="mailto:">
                                        Dúvidas? Entre em contato
                      </a>
                                    <span className="navbar-item">
                                        <a href="http://github.com/kkrico" target="_blank" rel="noopener noreferrer" className="button is-success is-inverted">
                                            <span className="icon">
                                                <i className="fab fa-github"></i>
                                            </span>
                                            <span>Made by Daniel Ramos</span>
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
                                <SurveyOptions model={this.props.location.state.model}></SurveyOptions>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        )
    }
}