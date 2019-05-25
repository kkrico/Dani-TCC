import React from "react";
import ReactDOM from "react-dom";
import "./styles.scss";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import { MainPage } from "./components/MainPage";
import { Survey } from "./components/Survey";
import { ThankYou } from "./components/ThankYou";

const rootElement = document.getElementById("root");

const App = () => {
    return <Router>
        <Switch>
            <Route exact path="/" component={MainPage} />
            <Route exact path="/pesquisa" component={Survey} />
            <Route exact path="/obrigado" component={ThankYou}></Route>
        </Switch>
    </Router>
}
ReactDOM.render(<App />, rootElement);