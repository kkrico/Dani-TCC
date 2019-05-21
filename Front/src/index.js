import React from "react";
import ReactDOM from "react-dom";
import "./styles.scss";

function App() {
    return (
        <section className="hero is-fullheight">
            <div className="hero-body">
                <div className="container has-text-centered">
                    <div className="column is-8 is-offset-2">
                        <div className="center-text">
                            <p className="title white shadow is-1">O QUE DEFINE ALGUÉM COMO CONFIÁVEL?</p>
                            <button className="facebook-button button is-medium"><i className="fab fa-facebook-f"></i></button>
                            <button className="twitter-button button is-medium"><i className="fab fa-twitter"></i></button>
                            <button className="google-button button is-medium"><i className="fab fa-google-plus-g"></i></button>
                            <button className="linkedin-button button is-medium"><i className="fab fa-linkedin-in"></i></button>
                            <br></br>
                            <br></br>
                            <p className="subtitle is-4 shadow">
                                Na tarefa seguir, você será apresentado a um conjunto de imagens  de políticos para classificar quem lhe parece mais competente. Você deve classifica-los o mais rápido que puder, de preferência em menos de 3 segundos. Para agilizar o processo, mantenha a mão sobre as setas esquerda e direita para poder responder mais rapidamente
                            </p>
                            <br />
                            <form>
                                <div className="field">
                                    <p className="control has-icons-left has-icons-right">
                                        <input className="input is-medium" type="email" placeholder="Email" />
                                        <span className="icon is-medium is-left">
                                            <i className="fas fa-envelope"></i>
                                        </span>
                                        <span className="icon is-medium is-right">
                                            <i className="fas fa-check"></i>
                                        </span>
                                    </p>
                                </div>
                                <a className="button is-large is-fullwidth" href="https://github.com/aldi/awesome-bulma-templates">
                                    <span className="icon is-medium">
                                        <i className="far fa-bell"></i>
                                    </span>
                                    <span>Começar o teste</span>
                                </a>
                                <br />
                            </form>
                        </div>
                    </div>
                </div>
            </div>
            <div className="hero-foot">
                <div className="container">
                    <div className="tabs is-centered">
                        <ul>
                        </ul>
                    </div>
                </div>
            </div>
        </section>
    );
}

const rootElement = document.getElementById("root");
ReactDOM.render(<App />, rootElement);