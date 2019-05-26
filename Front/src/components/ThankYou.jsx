import React from "react";
import { Redirect } from "react-router-dom";
import { API } from "../API/API";
import "./ThankYou";

export class ThankYou extends React.Component {
    componentDidMount() {

        if (typeof (this.props.location.state) == "undefined")
            return window.location = "/";

        API.post("/survey/endsurvey", this.props.location.state.answers);
    }

    render() {

        if (typeof (this.props.location.state) == "undefined")
            return <Redirect to="/" ></Redirect>;

        return (
            <section className="hero is-fullheight">
                <div className="hero-body">
                    <div className="container has-text-centered">
                        <div className="column is-8 is-offset-2">
                            <div className="center-text">
                                <p className="title white shadow is-1">Obrigado <span role="img" aria-label="Aperto de Maos">🤝</span></p>
                                <p className="shadow" style={{
                                    color: "white"
                                }}>pelo seu tempo. Sua pesquisa foi registrada com <strong style={{color: "green"}}>sucesso</strong>. Em caso de dúvidas, sugestões ou críticas, favor entrar em contato</p>
                                <br></br>
                                <a href="/" className="button">Voltar ao inicio</a>
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
        )
    }
}