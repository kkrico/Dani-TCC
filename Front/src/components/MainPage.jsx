import React from "react";
import { API } from "../API/API";

const socieeconomico = "SocioEconomic";

const getAll = (uris) => {
    return uris.map(uri => API.get(socieeconomico + "/" + uri));
}

const Select = ({ itens, label, onChange, name }) => {
    return <div className="field">
        <label className="label shadow">{label && label + " :"}</label>
        <div className="select" onChange={onChange}>
            <select name={name}>
                <option value="">Selecione</option>
                {
                    itens.map(item => <option key={item.id} value={item.id}>{item.description}</option>)
                }
            </select>
        </div>
    </div>
}

const calculateCssClass = (isActive) => {
    let className = "button is-large is-fullwidth";
    if (isActive)
        className += className + " is-loading"

    return className;
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
            loading: false,
            questionarioSocioEconomico: {
                ethnicity: null,
                gender: null,
                agegroup: null,
                familyincome: null,
                sexuality: null,
            }
        }

        this.handleChange = this.handleChange.bind(this);
        this.handlePost = this.handlePost.bind(this);
        this.onFinishSurveyRegister = this.onFinishSurveyRegister.bind(this);
        require("./mainpage.scss")
    }

    onFinishSurveyRegister(model) {
        this.setState({ loading: false });
        this.props.history.push({
            pathname: '/pesquisa',
            state: { model }
        })
    }

    handleChange(item) {

        const { questionarioSocioEconomico } = this.state;

        if (item.target.value === "")
            questionarioSocioEconomico[item.target.name] = null;
        else
            questionarioSocioEconomico[item.target.name] = parseInt(item.target.value);

        this.setState({
            questionarioSocioEconomico
        })
    }

    handlePost(evt) {

        evt.preventDefault();
        this.setState({ loading: true })
        API
            .post("/survey", this.state.questionarioSocioEconomico)
            .then(this.onFinishSurveyRegister);
        return false;
    }

    componentDidMount() {

        this.setState({ loading: true })
        Promise.all(
            getAll(["ethnicity", "gender", "agegroup", "familyincome", "sexuality"])
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
                                <p className="title white shadow is-1">O QUE DEFINE ALGUÉM COMO COMPETENTE?</p>
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
                                <form method="POST" onSubmit={this.handlePost}>
                                    <div className="columns">
                                        <div className="column">
                                            <Select itens={this.state.etnia} name="ethnicity" label="Etnia" onChange={this.handleChange} />
                                        </div>
                                        <div className="column">
                                            <Select itens={this.state.genero} name="gender" label="Genero" onChange={this.handleChange}></Select>
                                        </div>
                                        <div className="column">
                                            <Select itens={this.state.faixaetaria} name="agegroup" label="Faixa Etária" onChange={this.handleChange}></Select>
                                        </div>
                                        <div className="column">
                                            <Select itens={this.state.rendafamiliar} name="familyincome" label="Renda Familiar" onChange={this.handleChange}></Select>
                                        </div>
                                        <div className="column">
                                            <Select itens={this.state.sexualidade} label="Sexualidade" name="sexuality" onChange={this.handleChange}></Select>
                                        </div>
                                    </div>

                                    <button type="submit" className={calculateCssClass(this.state.loading)} href="https://github.com/aldi/awesome-bulma-templates">
                                        <span className="icon is-medium">
                                            <i className="far fa-bell"></i>
                                        </span>
                                        <span>Começar o teste</span>
                                    </button>
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