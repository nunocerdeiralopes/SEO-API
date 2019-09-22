import React, { Component } from 'react';
import LineChart from './Graph';
import AlertDismissable from './Alert';

export class History extends Component {
    static displayName = History.name;

    constructor(props) {
        super(props);
        this.state = {
            inputQuerySchedule: '',
            inputUrlSchedule: '',
            inputCountryDomainSchedule: 'co.uk',
            loading: true,
            scheduleItems: [],
            showGraph: false,
            GraphKeyword: null,
            showAlert: false,
            alert: null
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.fetchRecurringKeywords = this.fetchRecurringKeywords.bind(this);
        this.handleDelete = this.handleDelete.bind(this);
        this.renderScheduleTable = this.renderScheduleTable.bind(this);

        this.fetchRecurringKeywords();
    }

    async fetchRecurringKeywords() {
        await fetch('https://localhost:5001/api/RecurringKeyword')
            .then(response => response.json())
            .then(data => {
                this.setState({ scheduleItems: data, loading: false });
            });
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;
        const name = target.name;

        this.setState({
            [name]: value
        });
    }

    async handleDelete(e, id) {
        e.preventDefault();
        const response = await fetch('https://localhost:5001/api/RecurringKeyword/' + id, {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            }
        })
            .then(dataFetched => {
                this.setState({
                    showAlert: true,
                    alert: {
                        message: "Keyword deleted.",
                        type: "success",
                    }
                });
            })
            .catch(error => {
                console.log(error);
                this.setState({
                    showAlert: true,
                    alert: {
                        message: "Bad request.",
                        type: "danger",
                    }
                });
            });

        this.setState({ loading: true });
        this.fetchRecurringKeywords();
    }

    async handleGraph(e, id, query, url, countryDomain) {
        e.preventDefault();
        this.setState({
            showGraph: true,
            GraphKeyword: {
                RecurringKeywordId: id,
                Url: url,
                Query: query,
                CountryDomain: countryDomain
            }
        });
    }

    async handleSubmit(event) {
        event.preventDefault();

        //API request
        const response = await fetch('https://localhost:5001/api/RecurringKeyword', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                Query: this.state.inputQuerySchedule,
                Url: this.state.inputUrlSchedule,
                CountryDomain: this.state.inputCountryDomainSchedule
            })
        })
            .then(dataFetched => {
                this.setState({
                    showAlert: true,
                    alert: {
                        message: "Keyword added.",
                        type: "success",
                    }
                });
            })
            .catch(error => {
                console.log(error);
                this.setState({
                    showAlert: true,
                    alert: {
                        message: "Bad request.",
                        type: "danger",
                    }
                });
            });

        const data = await response.json();


        let scheduleItems = [...this.state.scheduleItems];

        scheduleItems.push({ inputQuerySchedule: this.state.inputQuerySchedule, inputUrlSchedule: this.state.inputUrlSchedule, inputCountryDomainSchedule: this.state.inputCountryDomainSchedule });

        this.setState({
            inputQuerySchedule: '',
            inputUrlSchedule: '',
            inputCountryDomainSchedule: 'co.uk',
            scheduleItems
        });

    }


    renderScheduleTable(scheduleItems) {
        return (
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>Query</th>
                        <th>Url</th>
                        <th>Country Domain</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {scheduleItems.map(item => {
                        return (
                            <tr key={item.recurringKeyworId}>
                                <td>{item.query}</td>
                                <td>{item.url}</td>
                                <td>{item.countryDomain}</td>
                                <td><button type="submit" className="btn btn-sm btn-primary" onClick={(e) => this.handleGraph(e, item.recurringKeyworId, item.query, item.url, item.countryDomain)}>Weekly Evolution</button>
                                    <button id={item.recurringKeyworId} type="button" className="close" aria-label="Close" onClick={(e) => this.handleDelete(e, item.recurringKeyworId)}>
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </td>
                            </tr>
                        );
                    })}
                </tbody>
            </table>
        )
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderScheduleTable(this.state.scheduleItems);

        let graph;
        if (this.state.showGraph) {
            graph = <LineChart GraphKeyword={this.state.GraphKeyword} />;
        }

        let alert;
        if (this.state.showAlert) {
            alert = <AlertDismissable type={this.state.alert.type} message={this.state.alert.message} />;
        }

        return (
            <div>
            {alert}
            <form onSubmit={this.handleSubmit}>
                <div className="form-row">
                    <div className="form-group col-md-3">
                        <label htmlFor="inputQueryScheduleID">Query</label>
                        <input type="text" className="form-control" id="inputQueryScheduleID" name="inputQuerySchedule" value={this.state.inputQuerySchedule} onChange={this.handleInputChange} required />
                    </div>
                    <div className="form-group col-md-5">
                        <label htmlFor="inputUrlScheduleID">Url</label>
                        <input type="text" className="form-control" id="inputUrlScheduleID" name="inputUrlSchedule" value={this.state.inputUrlSchedule} onChange={this.handleInputChange} required />
                    </div>
                    <div className="form-group col-md-4">
                        <label htmlFor="inputCountryDomainScheduleID">Country Domain</label>
                        <select id="inputCountryDomainScheduleID" name="inputCountryDomainSchedule" value={this.state.inputCountryDomainSchedule} className="form-control" onChange={this.handleInputChange} required>
                            <option value="co.uk">co.uk</option>
                            <option value="com">com</option>
                            <option value="co.au">co.au</option>
                        </select>
                    </div>
                </div>
                <div className="form-group row">
                    <div className="col-sm-10">
                        <button type="submit" className="btn btn-primary">Schedule daily check</button>
                    </div>
                </div>
            </form>
            {contents}
            {graph}
            </div>
        );
    }
}
