import React from "react";
import { API } from "../API/API";

const socieeconomico = "socieconomico";

const getAll = (uris) => {
    return uris.map(uri => API.get(socieeconomico + "/" + uri));
}

const Select = ({ itens, label }) => {

    return <div className="field">
        <label className="label shadow">{label && label + " :"}</label>
        <div className="select">
            <select>
                <option>Selecione</option>
                {
                    itens.map(item => <option key={item.id} value={item.id}>{item.description}</option>)
                }
            </select>
        </div>
        }
    </div>
}

export class MainPage extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            etnia: [],
            genero: [],
            faixaetaria: [],
            rendafamiliar: [],
            sexualidade: [],
            loading: false
        }
    }

    componentDidMount() {

        this.setState({ loading: true })
        Promise.all(
            getAll(["etnia", "genero", "faixaetaria", "rendafamiliar", "sexualidade"])
        ).then(itens => {
            const [etnia, genero, faixaetaria, rendafamiliar, sexualidade] = itens;

            this.setState({
                etnia,
                genero,
                faixaetaria,
                rendafamiliar,
                sexualidade,
                loading: false
            })
        })
    }

    render() {
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
                                <p className="subtitle shadow is-marginless">Dados socieconomicos: (opcionais) </p>
                                <br />
                                <form>
                                    <div className="columns">
                                        <div className="column">
                                            <Select itens={this.state.etnia} label="Etnia" />
                                        </div>
                                        <div className="column">
                                            <Select itens={this.state.genero} label="Genero"></Select>
                                        </div>
                                        <div className="column">
                                            <Select itens={this.state.faixaetaria} label="Faixa Etária"></Select>
                                        </div>
                                        <div className="column">
                                            <Select itens={this.state.rendafamiliar} label="Renda Familiar"></Select>
                                        </div>
                                        <div className="column">
                                            <Select itens={this.state.sexualidade} label="Sexualidade"></Select>
                                        </div>
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

}