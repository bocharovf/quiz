import { Component, OnInit } from '@angular/core';
import { ViewChild } from '@angular/core';
import { ViewContainerRef } from '@angular/core';
import { Input } from '@angular/core';
import { ComponentFactoryResolver } from '@angular/core';
import { Injector } from '@angular/core';
import { ComponentRef } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { Output } from '@angular/core';

import IQuestionComponentData from '../IQuestionComponentData';
import { QuestionSingleRightComponent } from '../question-single-right/question-single-right.component';
import { Answer } from '../../codegen/model.g';
import IQuestionComponent from '../IQuestionComponent';

/**
 * Container component to display appropriate question component.
 */
@Component({
  selector: 'quiz-flow-question',
  templateUrl: './quiz-flow-question.component.html',
  styleUrls: ['./quiz-flow-question.component.scss'],
  entryComponents: [ QuestionSingleRightComponent ]
})
export class QuizFlowQuestionComponent implements OnInit {

  /** Emits event when question is answered. */
  @Output() answered = new EventEmitter<Answer>();

  /** Gets current question component. */
  get questionComponent(): IQuestionComponent {
    return this.currentComponent
            ? <IQuestionComponent>this.currentComponent.instance
            : null;
  }

  /** Determines whether question component has selected answer. */
  answerAvailable: boolean;

  @ViewChild('questionContainer', { read: ViewContainerRef })
  private questionContainer: ViewContainerRef & IQuestionComponent;

  private currentComponent: ComponentRef<{}> = null;

  constructor(private resolver: ComponentFactoryResolver) {
  }

  ngOnInit() {
  }

  /**
   * Sets question component data.
   */
  @Input()
  set componentData(data: IQuestionComponentData) {
    if (!data) {
      return;
    }

    const component = this.createComponent(data);
    this.questionContainer.insert(component.hostView);
    this.updateCurrentComponent(component);
    this.initializeComponent();
  }

  /** Accept current answer. */
  acceptAnswer() {
    const answer = this.questionComponent.getAnswer();
    this.answered.emit(answer);
  }

  /**
   * Creates appropriate question component.
   * @param data Question component data.
   */
  private createComponent (data: IQuestionComponentData): ComponentRef<{}> {
    const inputProviders = Object.keys(data.inputs).map(inputName =>
      ({
        provide: inputName,
        useValue: data.inputs[inputName]
      })
    );

    const injector = Injector.create(inputProviders, this.questionContainer.parentInjector);
    const factory = this.resolver.resolveComponentFactory(data.component);
    const component = factory.create(injector);

    return component;
  }

  /**
   * Updates current question component with a new one.
   * @param component Question component to replace current one.
   */
  private updateCurrentComponent(component: ComponentRef<{}>) {
    if (this.currentComponent) {
      this.currentComponent.destroy();
    }

    this.currentComponent = component;
  }

  /**
   * Initializes question component.
   */
  private initializeComponent() {
    this.answerAvailable = false;
    this.questionComponent
        .answered
        .subscribe(isAvailable => this.answerAvailable = isAvailable);
  }
}
