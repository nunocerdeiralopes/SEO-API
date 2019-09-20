import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = {
            inputQuery: '',
            inputUrl: '',
            inputCountryDomain: 'co.uk',
            items: []
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

    async handleSubmit(event) {
        event.preventDefault();

        var params = [this.state.inputQuery, this.state.inputUrl, this.state.inputCountryDomain];
        var esc = encodeURIComponent;
        let positionFetched = '';


        //API request
        const response = await fetch('https://localhost:5001/api/google/' + params.map(k => esc(k) + "/").join(''))
        const data = await response.json();
        positionFetched = '' + data;


        let items = [...this.state.items];

        items.push({ inputQuery: this.state.inputQuery, inputUrl: this.state.inputUrl, inputCountryDomain: this.state.inputCountryDomain, positions: positionFetched });

        this.setState({
            inputQuery: '',
            inputUrl: '',
            inputCountryDomain: 'co.uk',
            items
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
                        {this.state.items.map(item => {
                            return (
                                <tr>
                                    <td>{item.inputQuery}</td>
                                    <td>{item.inputUrl}</td>
                                    <td>{item.inputCountryDomain}</td>
                                    <td>{item.positions}</td>
                                </tr>
                            );
                        })}
                    </tbody>
                </table>
            </div>
        );
    }
}
