import React from "react";
import { Redirect } from "react-router-dom";
import { UserPhoto } from "./UserPhoto";

export class SurveyOptions extends React.Component {
    constructor(props) {
        super(props);

        this.onUserPhotoClick = this.onUserPhotoClick.bind(this);
    }

    onUserPhotoClick(optionIndex, option, interVal, question) {
        const { voteOnLeft, voteOnRight } = this.props;
        optionIndex % 2 === 0 ? voteOnLeft(option.valueAnswerId, interVal, question.answerId) :
            voteOnRight(option.valueAnswerId, interVal, question.answerId);
    }

    render() {
        const { model, rightWasPressed, leftWasPressed, lastAnswer } = this.props;
        return <React.Fragment>
            <p className="title white shadow is-1">{model.surveyCommand}</p>
            <br></br>
            {
                model.questions.map((question, questionIndex) => <div className="columns" key={questionIndex}>
                    {
                        question.options.map((option, optionIndex) => {

                            var leftItens = this.props.model.questions.filter(m => m.answerId > lastAnswer)
                            if (leftItens.length === 0)
                                return <Redirect key={optionIndex} to={{
                                    pathname: "/obrigado",
                                    state: { answers: this.props.selectedAnswers }
                                }}></Redirect>
                            const isTheFirstOne = (lastAnswer == null && questionIndex === 0);

                            var isTheNextOne = leftItens[0].answerId === question.answerId;
                            if ((!isTheFirstOne && lastAnswer == null) || (!isTheNextOne && lastAnswer !== null))
                                return null;
                            return <UserPhoto option={option} optionIndex={optionIndex}
                                key={optionIndex}
                                onUserPhotoClick={
                                    (optionIndex, option, interVal) => this.onUserPhotoClick(optionIndex, option, interVal, question)}
                                shouldBeingShown={questionIndex === 0}
                                leftWasPressed={leftWasPressed}
                                rightWasPressed={rightWasPressed}>
                            </UserPhoto>
                        })
                    }
                </div>)
            }

        </React.Fragment>
    };
}