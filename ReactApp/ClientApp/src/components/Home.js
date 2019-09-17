import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = {
            inputQuery: '',
            inputUrl: '',
            inputCountryDomain: 'co.uk'
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;
        const name = target.name;

        this.setState({
            [name]: value
        });
    }

    handleSubmit() {

        fetch('https://localhost:5001/api/google/')
            .then(response => response.json())
            .then(data => {
                this.setState({ forecasts: data, loading: false });
            });


    }

    render() {
        return (
            <div>
                <form onSubmit={this.handleSubmit}>
                    <div className="form-row">
                        <div className="form-group col-md-3">
                            <label htmlFor="inputQueryID">Query</label>
                            <input type="text" className="form-control" id="inputQueryID" name="inputQuery" value={this.state.inputQuery} onChange={this.handleInputChange} required />
                    </div>
                        <div className="form-group col-md-5">
                            <label htmlFor="inputUrlID">Url</label>
                            <input type="text" className="form-control" id="inputUrlID" name="inputUrl" value={this.state.inputUrl} onChange={this.handleInputChange} required />
                    </div>
                        <div className="form-group col-md-4">
                            <label htmlFor="inputCountryDomainID">Country Domain</label>
                            <select id="inputCountryDomainID" name="inputCountryDomain" value={this.state.inputCountryDomain} className="form-control" onChange={this.handleInputChange} required>
                            <option value="co.uk">co.uk</option>
                            <option value="com">com</option>
                            <option value="co.au">co.au</option>
                        </select>
                    </div>
                </div>
                    <div className="form-group row">
                        <div className="col-sm-10">
                            <button type="submit" className="btn btn-primary">Get Positions</button>
                    </div>
                </div>
                </form>
                <table className='table table-striped'>
                    <thead>
                        <tr>
                            <th>Query</th>
                            <th>Url</th>
                            <th>Country Domain</th>
                            <th>Position</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        );
    }
}
