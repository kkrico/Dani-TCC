import React from "react";
export class UserPhoto extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            interVal: 0,
        };
        this.onInterval = this.onInterval.bind(this);
    }
    onInterval() {
        let { interVal } = this.state;
        interVal += 1;
        this.setState({
            interVal
        });
    }
    componentDidMount() {
        var interValId = setInterval(this.onInterval, 1000);
        this.setState({ interValId: interValId });
    }
    componentWillUnmount() {
        clearInterval(this.state.interValId);
    }
    
    componentWillReceiveProps(nextProps) {
        const { optionIndex, option, onUserPhotoClick } = nextProps;
        if (nextProps.rightWasPressed && optionIndex % 2 !== 0) {
            onUserPhotoClick(optionIndex, option, this.state.interVal);
        }
        if (nextProps.leftWasPressed && optionIndex % 2 === 0) {
            onUserPhotoClick(optionIndex, option, this.state.interVal);
        }
    }
    render() {
        const { optionIndex, option, onUserPhotoClick } = this.props;
        const imageBase64 = "data:image/png;base64, " + option.base64Photo;
        return (<div key={optionIndex} className="column">
            <img src={imageBase64} alt="Candidato" title="Este candidato parece mais competente para você? Se sim, clique nele para votar" className="userphoto animated grow" onClick={() => onUserPhotoClick(optionIndex, option, this.state.interVal)}></img>
            <br></br>
            <button className="button" onClick={() => onUserPhotoClick(optionIndex, option, this.state.interVal)}>
                Candidato</button>
        </div>);
    }
}
