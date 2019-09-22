import React, { Component } from 'react';

class AlertDismissable extends Component {
    constructor(props, context) {
        super(props, context);

        this.handleDismiss = this.handleDismiss.bind(this);

        this.state = {
            show: true,
            message: this.props.message,
            type: this.props.type,
        };
    }

    handleDismiss() {
        this.setState({ show: false });
    }


    render() {
        if (!this.state.show)
            return null;

        return (
            <div className={`alert alert-${this.state.type} alert-dismissible fade show`} role="alert">
                    <button type="button" className="close" data-dismiss="alert" aria-label="Close" onClick={this.handleDismiss} >
                        <span aria-hidden="true">&times;</span>
                    </button>
                    {this.state.message}
                </div>
            );
        }
    
}

export default AlertDismissable;